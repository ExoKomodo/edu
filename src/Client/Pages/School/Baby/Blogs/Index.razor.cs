using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;

namespace Client.Pages.School.Baby.Blogs
{
    public partial class Index
    {
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                _blogs = new Dictionary<string, IEnumerable<Blog>>();
                foreach (
                    var blogKind in new List<string>
                    {
                        "baby-carrier",
                    }
                )
                {
                    _blogService.Id = blogKind;
                    _blogs[blogKind] = await _blogService.GetAsync();
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private IDictionary<string, IEnumerable<Blog>> _blogs { get; set; }
        [Inject]
        private BlogService _blogService { get; set; }
    }
}
