using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix.Reference
{
	public class FileServer : Program
	{
		public override void Start(Process process)
		{
			process.Services.Add(new FileService());

			base.Start(process);
		}

		public override void Wait(Process process)
		{
			foreach (var service in process.Services.OfType<FileService>())
			{
				while (service.Requests.Count != 0)
				{
					var request = service.Requests.Dequeue();

					if (request is CreateFileRequest createFileRequest)
					{
						service.Responses.Enqueue(new CreateFileResponse
						{
							Request = createFileRequest
						});
					}
				}
			}

			base.Wait(process);
		}

		public class FileService : Service
		{
			
		}

		public class CreateFolderRequest : ServiceRequest
		{
			public Folder Parent;
			public Folder Folder;
		}

		public class CreateFolderResponse : ServiceResponse
		{
			public CreateFolderRequest Request;
			public CreateFolderResult Result;
		}

		public enum CreateFolderResult
		{
			FolderCreated,
			FolderExists,
			ParentFolderNotFound,
			InsufficientSpace
		}

		public class CreateFileRequest : ServiceRequest
		{
			public Folder Folder;
			public File File;
		}

		public class CreateFileResponse : ServiceResponse
		{
			public CreateFileRequest Request;
			public CreateFileResult Result;
		}

		public enum CreateFileResult
		{
			FileCreated,
			FileExists,
			FolderNotFound,
			InsufficientSpace
		}

		public class SaveFileRequest : ServiceRequest
		{
			public Folder Folder;
			public File File;
		}

		public class SaveFileResponse : ServiceResponse
		{
			public SaveFileRequest Request;
		}

		public enum SaveFileResult
		{
			FileCreated,
			FileReplaced,
			FolderNotFound,
			InsufficientSpace
		}

		public class GetFilesRequest : ServiceRequest
		{
			public Folder Folder;
		}

		public class GetFilesResponse : ServiceResponse
		{
			public GetFilesRequest Request;
			public GetFilesResult Result;
			public File[] Files;
		}

		public enum GetFilesResult
		{
			Success,
			FolderNotFound,
		}

		public class GetFoldersRequest : ServiceRequest
		{
			public Folder Folder;
		}

		public class GetFoldersResponse : ServiceResponse
		{
			public GetFoldersRequest Request;
			public GetFoldersResult Result;
			public Folder[] Folders;
		}

		public enum GetFoldersResult
		{
			Success,
			FolderNotFound,
		}
	}
}
