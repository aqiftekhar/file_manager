using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Repositories.FileRepository;
using FileManagerBackend.Services.Data;
using File = FileManagerBackend.Models.File;
using FileManagerBackend.Repositories.Exceptions;

namespace FileManagerBackend.Tests
{
    [TestClass]
    public class FileRepositoryTests
    {
        private FileManagementContext _context;
        private FileRepository _fileRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<FileManagementContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;

            _context = new FileManagementContext(options);

            // Seed data with all required properties

            _context.Volumes.Add(new Volume
            {
                Name = "Volume1"
            });

            _context.Files.Add(new File
            {
                Name = "File1",
                VolumeId = 1,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                BinaryData = new byte[] { 0x01, 0x02, 0x03 },
            });
            _context.SaveChanges();

            // Initialize the repository with the in-memory context
            _fileRepository = new FileRepository(_context);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            //Dispose and remove in-memory context
            _context?.Dispose();
        }

        [TestMethod]
        public async Task GetFileAsync_ReturnsFile_WhenFileExists()
        {
            // Arrange
            var fileId = 1;

            // Act
            var result = await _fileRepository.GetFileAsync(fileId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fileId, result.Id);
            Assert.AreEqual("File1", result.Name);
        }

        [TestMethod]
        public async Task GetFileAsync_ThrowsRepositoryException_WhenExceptionThrown()
        {
            // Arrange
            var fileId = 2;
            _context.Dispose(); 

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(() => _fileRepository.GetFileAsync(fileId));

        }

        [TestMethod]
        public async Task GetFilesAsync_ReturnListOfFiles()
        {

            // Arrange
            var file = new File
            {
                Name = "File2",
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                BinaryData = new byte[] { 0x04, 0x05, 0x06 }
            };
            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            // Act
            var result = await _fileRepository.GetFilesAsync();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(file => file.Name == "File1"));

        }
        [TestMethod]
        public async Task GetFilesAsync_ThrowsRepositoryException_WhenExceptionThrown()
        {
            // Arrange
            _context.Dispose(); 

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(() => _fileRepository.GetFilesAsync());

        }
        [TestMethod]
        public async Task AddFileAsync_AddsFile_WhenVolumeExists()
        {
            // Arrange
            var file = new File
            {
                Name = "File2",
                VolumeId = 1,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                BinaryData = new byte[] { 0x04, 0x05, 0x06 }
            };

            // Act
            var result = await _fileRepository.AddFileAsync(file);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(file.Id, result.Id);
            Assert.AreEqual(file.Name, result.Name);
        }
        [TestMethod]
        public async Task AddFileAsync_ThrowsArgumentException_WhenVolumeDoesNotExists()
        {
            // Arrange

            var file = new File
            {
                Name = "File2",
                VolumeId = 900, 
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                BinaryData = new byte[] { 0x04, 0x05, 0x06 }
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(async () => await _fileRepository.AddFileAsync(file));
        }
        [TestMethod]
        public async Task UpdateFileAsync_UpdatesFile_WhenFileExists()
        {
            // Arrange
            var file = await _context.Files.FindAsync(1); // Load the existing file from context
            if (file == null)
            {
                Assert.Fail("File with ID 1 does not exist.");
            }

            file.Name = "UpdateFile1";
            file.ModifyDate = DateTime.UtcNow;

            // Act
            await _fileRepository.UpdateFileAsync(file);

            // Assert
            var result = await _context.Files.FindAsync(file.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(file.Name, result.Name);
        }

        [TestMethod]
        public async Task DeleteFileAsync_DeletesFile_WhenFileExists()
        {
            // Arrange
            var fileId = 1;

            // Act
            await _fileRepository.DeleteFileAsync(fileId);

            // Assert
            var result = await _context.Files.FindAsync(fileId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteFileAsync_ThrowsRepositoryException_WhenFileExists()
        {
            // Arrange
            var fileId = 2;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(async () => await _fileRepository.DeleteFileAsync(fileId));
        }
        [TestMethod]
        public async Task GetFilesByVolumeIdAsync_ReturnsFiles_FormSpecificVolumeId()
        {
            // Arrange
            var volumeId = 1;
            var file = new File
            {
                Name = "File2",
                VolumeId = volumeId,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                BinaryData = new byte[] { 0x04, 0x05, 0x05 }
            };
            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            // Act
            var result = await _fileRepository.GetFilesByVolumeIdAsync(volumeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(file => file.Id == 2));
        }

        [TestMethod]
        public async Task GetFilesByVolumeIdAsync_ThrowsRepositoryException_WhenExceptionThrown()
        {
            // Arrange
            var volumeId = 1;
            _context.Dispose(); 

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(() => _fileRepository.GetFilesByVolumeIdAsync(volumeId));
        }
        [TestMethod]
        public async Task GetVolumesWithFilesAndTagsAsync_ReturnsVolumeWithFilesAndTags()
        {
            // Arrange
            var volume = new Volume
            {
                Name = "Volume1",
                Files = new List<File> {
                    new File {
                        Name = "File1",
                        CreateDate = DateTime.UtcNow,
                        ModifyDate = DateTime.UtcNow,
                        BinaryData = new byte[] { 0x07, 0x08, 0x09 },
                        TagAssignments = new List<TagAssignment> {
                            new TagAssignment {
                                FileId = 1, TagId = 1
                            },
                            new TagAssignment {
                                FileId = 1, TagId = 2
                            },
                        }
                    }
                }
            };
            _context.Volumes.Add(volume);
            await _context.SaveChangesAsync();

            // Act
            var result = await _fileRepository.GetVolumesWithFilesAndTagsAsync();

            // Assert
            Assert.IsNotNull(result);
            var volumesList = result.ToList();

            ;
            Assert.AreEqual(2, volumesList.Count);

            var filesList = volumesList.First().Files.ToList();
            Assert.AreEqual(1, filesList.Count);
            Assert.AreEqual("File1", filesList.First().Name);

        }

        [TestMethod]
        public async Task GetVolumesWithFilesAndTagsAsync_ThrowsRepositoryException_WhenExceptionThrown()
        {
            // Arrange
            _context.Dispose();

            // Act & Assert
            await Assert.ThrowsExceptionAsync<RepositoryException>(() => _fileRepository.GetVolumesWithFilesAndTagsAsync());
        }

    }
}