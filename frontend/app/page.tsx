import Image from "next/image";

export default function Home() {
  return (
    <div className="flex h-screen max-h-screen">
      <section className="container my-auto">
        <div className="sub-container max-w-[860px] flex-1 flex-col py-10 bg-slate-600">
          Hello
        </div>
        <div>
          test
        </div>
      </section>
    </div>

    // <div className="container mx-auto p-4 rounded-lg shadow-md">
    //     <div className="flex justify-between items-center mb-4">
    //         <div className="flex space-x-4">
    //             <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded">
    //                 <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"></path></svg>
    //             </button>
    //             <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded">
    //                 <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path></svg>
    //             </button>
    //             <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded">
    //                 <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path></svg>
    //             </button>
    //             <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded">
    //                 <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 5l7 7-7 7M5 5l7 7-7 7"></path></svg>
    //             </button>
    //         </div>
    //         <div>
    //             <input type="text" placeholder="Search..." className="border rounded-lg px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500"/>
    //             <button className="ml-2 bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded">
    //                 <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
    //             </button>
    //         </div>
    //     </div>
    //     <div className="grid grid-cols-2 gap-4">
    //         <div>
    //             <div className="mb-2">
    //                 <label htmlFor="volume" className="block text-gray-700 text-sm font-bold mb-2">Volume</label>
    //                 <select id="volume" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline">
    //                     <option value="personal">Personal</option>
    //                     <option value="work">Work</option>
    //                     <option value="other">Other</option>
    //                 </select>
    //             </div>
    //             <div className="mb-2">
    //                 <label htmlFor="filter" className="block text-gray-700 text-sm font-bold mb-2">Filter description</label>
    //                 <input type="text" id="filter" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"/>
    //             </div>
    //             <ul className="list-none">
    //                 <li className="py-2 border-b border-gray-200">File Name 1</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 2</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 3</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 4</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 5</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 6</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 7</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 8</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 9</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 10</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 11</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 12</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 13</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 14</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 15</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 16</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 17</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 18</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 19</li>
    //                 <li className="py-2 border-b border-gray-200">File Name 20</li>
    //             </ul>
    //         </div>
    //         <div>
    //             <div className="mb-2">
    //                 <label htmlFor="title" className="block text-gray-700 text-sm font-bold mb-2">Title</label>
    //                 <input type="text" id="title" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"/>
    //             </div>
    //             <textarea className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline resize-none h-48" placeholder="Content..."></textarea>
    //             <div className="flex items-center mt-4">
    //                 <input type="checkbox" id="save-paper" className="form-checkbox h-5 w-5 text-gray-600 focus:ring-blue-500"/>
    //                 <label htmlFor="save-paper" className="ml-2 text-gray-700">Save Paper</label>
    //             </div>
    //             <div className="mb-2 mt-4">
    //                 <label htmlFor="tags" className="block text-gray-700 text-sm font-bold mb-2">Tags</label>
    //                 <input type="text" id="tags" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Tags go here"/>
    //             </div>
    //             <div className="mb-2">
    //                 <label htmlFor="created-date" className="block text-gray-700 text-sm font-bold mb-2">Created Date</label>
    //                 <input type="date" id="created-date" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"/>
    //             </div>
    //             <div className="flex space-x-4 mt-4">
    //                 <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
    //                     Show PDF
    //                 </button>
    //                 <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
    //                     Upload New
    //                 </button>
    //                 <button className="bg-gray-200 hover:bg-gray-300 text-gray-700 font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
    //                     Download
    //                 </button>
    //             </div>
    //         </div>
    //     </div>
    // </div>
  );
}
