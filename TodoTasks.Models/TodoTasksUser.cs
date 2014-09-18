namespace TodoTasks.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class TodoTasksUser : IdentityUser
    {
        private ICollection<Category> categories;

        public TodoTasksUser()
        {
            this.categories = new HashSet<Category>();
        }

        public virtual ICollection<Category> Categories 
        {
            get
            {
                return this.categories;
            }

            set
            {
                this.categories = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TodoTasksUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
