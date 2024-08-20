using Moq;
using FileManagerBackend.Controllers;
using FileManagerBackend.Models;
using FileManagerBackend.Repositories.FileRepository;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using File = FileManagerBackend.Models.File;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerBackend.Tests
{
    [TestClass]
    public class FileControllerTests
    {
        private Mock<IFileRepository> _fileRepositoryMock;
        private FileController _fileController;

        [TestInitialize]
        public void Initialize()
        {
            _fileRepositoryMock = new Mock<IFileRepository>();
            _fileController = new FileController(_fileRepositoryMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up any resources
            _fileRepositoryMock = null;
            _fileController = null;
        }

        [TestMethod]
        public async Task GetFiles_ShouldReturnOKResult_WhenFilesExist()
        {
            // Arrange
            var files = new List<File>{
                new File { Name = "File 1", VolumeId = 1 },
                new File { Name = "File 2", VolumeId = 2 }
            };

            _fileRepositoryMock.Setup(repo => repo.GetFilesAsync()).ReturnsAsync(files);

            // Act
            var result = await _fileController.GetFiles() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(files, result.Value);
        }

        [TestMethod]
        public async Task GetFiles_ShouldHandleException_WhenExceptionOccurs()
        {
            // Arrange
            var exceptionMessage = "Test exception";
            _fileRepositoryMock.Setup(repo => repo.GetFilesAsync()).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _fileController.GetFiles() as ObjectResult;

            // Assert
            Assert.IsNotNull(result, "Result is not of type ObjectResult");

            // Check the status code
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);

            // Check the error message
            var errorMessage = result.Value as string;
            Assert.IsNotNull(errorMessage, "Error message is null");
            Assert.IsTrue(errorMessage.Contains(exceptionMessage), "Error message does not contain expected exception message");
        }


        [TestMethod]
        public async Task GetFile_ReturnsOKResult_WhenFileExists()
        {
            // Arrange
            var file = new File { Name = "File1", VolumeId = 1 };
            _fileRepositoryMock.Setup(repo => repo.GetFileAsync(It.IsAny<int>())).ReturnsAsync(file);

            // Act
            var result = await _fileController.GetFile(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(file, result.Value);
        }

        [TestMethod]
        public async Task GetFile_ShouldHandleException_WhenFileDoesNotExist()
        {
            // Arrange
            var exceptionMessage = "File not found";
            _fileRepositoryMock.Setup(repo => repo.GetFileAsync(It.IsAny<int>())).ThrowsAsync(new ArgumentException(exceptionMessage));

            // Act
            var result = await _fileController.GetFile(999) as ObjectResult;

            // Assert
            Assert.IsNotNull(result, "Result is not of type ObjectResult");

            // Check the status code
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);

            // Check the error message
            var errorMessage = result.Value as string;
            Assert.IsNotNull(errorMessage, "Error message is null");
            Assert.IsTrue(errorMessage.Contains(exceptionMessage), "Error message does not contain expected exception message");
        }


        [TestMethod]
        public async Task CreateFile_ReturnsCreatedAtAction_WhenFileIsCreated()
        {
            // Arrange
            var inputFile = new File
            {
                Name = "file1",
                VolumeId = 1,
                Description = "Test File",
                SavePaper = false,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                BinaryData = new byte[] { 1, 2, 3 }
            };

            var createdFile = new File
            {
                Name = inputFile.Name,
                VolumeId = inputFile.VolumeId,
                Description = inputFile.Description,
                SavePaper = inputFile.SavePaper,
                CreateDate = inputFile.CreateDate,
                ModifyDate = inputFile.ModifyDate,
                BinaryData = inputFile.BinaryData
            };

            _fileRepositoryMock.Setup(repo => repo.AddFileAsync(It.IsAny<File>())).ReturnsAsync(createdFile);

            var formFileMock = new Mock<IFormFile>();
            var fileContent = new byte[] { 1, 2, 3 };
            var fileName = "file1.PDF";
            var stream = new MemoryStream(fileContent);
            formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            formFileMock.Setup(f => f.FileName).Returns(fileName);
            formFileMock.Setup(f => f.Length).Returns(stream.Length);

            // Act
            var result = await _fileController.CreateFile(formFileMock.Object, JsonSerializer.Serialize(inputFile));

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult, "Result is not of type CreatedAtActionResult");

            var returnedFile = createdAtActionResult.Value as File;
            Assert.IsNotNull(returnedFile, "Returned value is not of type File");

            // Assert most of the values individually because File.Id is auto generated in database
            // Assert should not assert BinaryData as it has different representation in database
            Assert.AreEqual(createdFile.Id, returnedFile.Id);
            Assert.AreEqual(createdFile.Name, returnedFile.Name);
            Assert.AreEqual(createdFile.VolumeId, returnedFile.VolumeId);
            Assert.AreEqual(createdFile.Description, returnedFile.Description);
            Assert.AreEqual(createdFile.SavePaper, returnedFile.SavePaper);

            Assert.IsTrue(Math.Abs((createdFile.CreateDate - returnedFile.CreateDate).TotalSeconds) < 1,
                "CreateDate property differs between createdFile and returnedFile");

            Assert.IsTrue(Math.Abs((createdFile.ModifyDate - returnedFile.ModifyDate).TotalSeconds) < 1,
                "ModifyDate property differs between createdFile and returnedFile");
        }

        [TestMethod]
        public async Task CreateFile_ShouldHandleException_WhenCreationFails()
        {
            // Arrange
            var exceptionMessage = "Creation failed";
            _fileRepositoryMock.Setup(repo => repo.AddFileAsync(It.IsAny<File>())).ThrowsAsync(new InvalidOperationException(exceptionMessage));

            var formFileMock = new Mock<IFormFile>();
            var fileContent = new byte[] { 1, 2, 3 };
            var fileName = "file1.PDF";
            var stream = new MemoryStream(fileContent);
            formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            formFileMock.Setup(f => f.FileName).Returns(fileName);
            formFileMock.Setup(f => f.Length).Returns(stream.Length);

            var inputFile = new File
            {
                Name = "file1",
                VolumeId = 1,
                Description = "Test File",
                SavePaper = false,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
            };

            // Act
            var result = await _fileController.CreateFile(formFileMock.Object, JsonSerializer.Serialize(inputFile)) as ObjectResult;

            // Assert
            Assert.IsNotNull(result, "Result is not of type ObjectResult");

            // Check the status code
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);

            // Check the error message
            var errorMessage = result.Value as string;
            Assert.IsNotNull(errorMessage, "Error message is null");
            Assert.IsTrue(errorMessage.Contains(exceptionMessage), "Error message does not contain expected exception message");
        }


        [TestMethod]
        public async Task UpdateFile_ReturnsOkResult_WhenFileUpdated()
        {
            // Arrange
            var file = new File { Id = 1, Name = "File 1 updated from test" };
            _fileRepositoryMock.Setup(repo => repo.UpdateFileAsync(It.IsAny<File>())).Returns(Task.CompletedTask);

            // Act
            var result = await _fileController.UpdateFile(1, file) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("File updated successfully.", result.Value);
        }

        [TestMethod]
        public async Task DeleteFile_ReturnsOkResult_WhenFileIsDeleted()
        {
            // Arrange
            _fileRepositoryMock.Setup(repo => repo.DeleteFileAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _fileController.DeleteFile(1) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(200, result?.StatusCode);
            Assert.AreEqual("File deleted successfully.", result?.Value);
        }

        [TestMethod]
        public async Task GetFilesByVolumeId_ReturnsOkResult_WhenFilesExist()
        {
            // Arrange
            var files = new List<File> { new File { Id = 1, Name = "File1", VolumeId = 1 } };
            _fileRepositoryMock.Setup(repo => repo.GetFilesByVolumeIdAsync(It.IsAny<int>())).Returns(Task.FromResult((IEnumerable<File>)files));

            // Act
            var result = await _fileController.GetFilesByVolumeId(files[0].Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(files, ((OkObjectResult)result).Value);
        }

        [TestMethod]
        public async Task GetVolumesWithFilesAndTags_ReturnsOkResult_WhenFilesExist()
        {
            // Arrange
            var volumes = new List<Volume> { new Volume { Name = "volume1" } };
            _fileRepositoryMock.Setup(repo => repo.GetVolumesWithFilesAndTagsAsync()).ReturnsAsync(volumes);

            // Act
            var result = await _fileController.GetVolumesWithFilesAndTags() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(volumes, result.Value);
        }

        [TestMethod]
        public async Task GetVolumesWithFilesAndTags_ShouldHandleException_WhenExceptionOccurs()
        {
            // Arrange
            var exceptionMessage = "Test exception";
            _fileRepositoryMock.Setup(repo => repo.GetVolumesWithFilesAndTagsAsync())
                .ThrowsAsync(new InvalidOperationException(exceptionMessage));

            // Act
            var result = await _fileController.GetVolumesWithFilesAndTags() as ObjectResult;

            // Assert
            Assert.IsNotNull(result, "Result is not of type ObjectResult");

            // Check the status code
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);

            // Check the error message
            var errorMessage = result.Value as string;
            Assert.IsNotNull(errorMessage, "Error message is null");
            Assert.IsTrue(errorMessage.Contains(exceptionMessage), "Error message does not contain expected exception message");
        }

    }
}
