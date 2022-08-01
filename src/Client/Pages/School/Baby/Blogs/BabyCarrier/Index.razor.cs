using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;

namespace Client.Pages.School.Baby.Blogs.BabyCarrier
{
    public partial class Index
    {
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                _blogService.Id = "baby-carrier";
                _blogs = await _blogService.GetAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private IEnumerable<Blog> _blogs { get; set; }
        [Inject]
        private BlogService _blogService { get; set; }
    }
}
