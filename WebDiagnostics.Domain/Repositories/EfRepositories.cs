
using System.Collections.Generic;
using System.Linq;
using WebDiagnostics.Domain.Interfaces;
using System;
namespace WebDiagnostics.Domain.Repositories
{
				
#region Example CRUD operations

		public partial class ExampleRepository : IEntityRepository<Example>
		{
		
		    private readonly IContext _context;
			   
			public ExampleRepository(IContext context)
			{
				_context = context;
			}

			public virtual IEnumerable<Example> Examples
			{
				get { return _context.Examples; }
			}
     
			public virtual IEnumerable<Example> All()
			{
				return _context.Examples; 
			}

			public virtual Example GetById(object id)
			{     
				    return All().Where(x => x.ExampleId == id.ToString()).FirstOrDefault();    
										
			}

			public virtual Example Save(Example item)
			{     
				bool updateRequired = false;
	            var id = item.ExampleId;
				
	            if(!string.IsNullOrEmpty(id))
	            {
		            updateRequired = true;
	            }
			
			
			
				if(updateRequired)
				{
					var itemToUpdate = GetById(item.ExampleId);
					if(itemToUpdate != null)
					{
						//set the properties and update
						SetUpdateProperties(ref itemToUpdate, item);
						_context.Save();
					}
					throw new ApplicationException("Item does not exist");
				}
				else 
				{
					_context.Examples.Add(item);
					_context.Save();
				}
				
				return item;

			
			}

			public virtual bool Delete(Example entity)
            {
				throw new System.NotImplementedException();	
			              		   
			}//end method

			//method for setting simple properties of a type when update is required
			public virtual void SetUpdateProperties(ref Example itemToUpdate, Example changedEntity)
			{
			   if(itemToUpdate.ExampleDesc != changedEntity.ExampleDesc && changedEntity.ExampleDesc != null)
			   {
				   itemToUpdate.ExampleDesc = changedEntity.ExampleDesc;
			   }				
							
			} 

		}

#endregion

	    		
#region TaskMessage CRUD operations

		public partial class TaskMessageRepository : IEntityRepository<TaskMessage>
		{
		
		    private readonly IContext _context;
			   
			public TaskMessageRepository(IContext context)
			{
				_context = context;
			}

			public virtual IEnumerable<TaskMessage> TaskMessages
			{
				get { return _context.TaskMessages; }
			}
     
			public virtual IEnumerable<TaskMessage> All()
			{
				return _context.TaskMessages; 
			}

			public virtual TaskMessage GetById(object id)
			{     
				    return All().Where(x => x.TaskMessageId == new Guid(id.ToString())).FirstOrDefault();
										
			}

			public virtual TaskMessage Save(TaskMessage item)
			{     
				bool updateRequired = false;
	            var id = item.TaskMessageId;
			
			
				
				if(id != Guid.Empty)
				{
					updateRequired = true;
				}
			
				if(updateRequired)
				{
					var itemToUpdate = GetById(item.TaskMessageId);
					if(itemToUpdate != null)
					{
						//set the properties and update
						SetUpdateProperties(ref itemToUpdate, item);
						_context.Save();
					}
					throw new ApplicationException("Item does not exist");
				}
				else 
				{
					_context.TaskMessages.Add(item);
					_context.Save();
				}
				
				return item;

			
			}

			public virtual bool Delete(TaskMessage entity)
            {
				throw new System.NotImplementedException();	
			              		   
			}//end method

			//method for setting simple properties of a type when update is required
			public virtual void SetUpdateProperties(ref TaskMessage itemToUpdate, TaskMessage changedEntity)
			{
			   if(itemToUpdate.MessageContent != changedEntity.MessageContent && changedEntity.MessageContent != null)
			   {
				   itemToUpdate.MessageContent = changedEntity.MessageContent;
			   }				
			   if(itemToUpdate.UserId != changedEntity.UserId && changedEntity.UserId != null)
			   {
				   itemToUpdate.UserId = changedEntity.UserId;
			   }				
			   if(itemToUpdate.Success != changedEntity.Success && changedEntity.Success != null)
			   {
				   itemToUpdate.Success = changedEntity.Success;
			   }				
			   if(itemToUpdate.MessageDate != changedEntity.MessageDate && changedEntity.MessageDate != null)
			   {
				   itemToUpdate.MessageDate = changedEntity.MessageDate;
			   }				
			   if(itemToUpdate.ExceptionDetail != changedEntity.ExceptionDetail && changedEntity.ExceptionDetail != null)
			   {
				   itemToUpdate.ExceptionDetail = changedEntity.ExceptionDetail;
			   }				
							
			} 

		}

#endregion

	    		
#region User CRUD operations

		public partial class UserRepository : IEntityRepository<User>
		{
		
		    private readonly IContext _context;
			   
			public UserRepository(IContext context)
			{
				_context = context;
			}

			public virtual IEnumerable<User> Users
			{
				get { return _context.Users; }
			}
     
			public virtual IEnumerable<User> All()
			{
				return _context.Users; 
			}

			public virtual User GetById(object id)
			{     
				    return All().Where(x => x.UserId == Convert.ToInt32(id)).FirstOrDefault();
										
			}

			public virtual User Save(User item)
			{     
				bool updateRequired = false;
	            var id = item.UserId;
			
				
				if(Convert.ToInt32(id) != 0)
				{
					updateRequired = true;
				}
			
			
				if(updateRequired)
				{
					var itemToUpdate = GetById(item.UserId);
					if(itemToUpdate != null)
					{
						//set the properties and update
						SetUpdateProperties(ref itemToUpdate, item);
						_context.Save();
					}
					throw new ApplicationException("Item does not exist");
				}
				else 
				{
					_context.Users.Add(item);
					_context.Save();
				}
				
				return item;

			
			}

			public virtual bool Delete(User entity)
            {
				throw new System.NotImplementedException();	
			              		   
			}//end method

			//method for setting simple properties of a type when update is required
			public virtual void SetUpdateProperties(ref User itemToUpdate, User changedEntity)
			{
			   if(itemToUpdate.Username != changedEntity.Username && changedEntity.Username != null)
			   {
				   itemToUpdate.Username = changedEntity.Username;
			   }				
			   if(itemToUpdate.UserLoginId != changedEntity.UserLoginId && changedEntity.UserLoginId != null)
			   {
				   itemToUpdate.UserLoginId = changedEntity.UserLoginId;
			   }				
							
			} 

		}

#endregion

	    	
	
}


