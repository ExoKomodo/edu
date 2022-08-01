using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Client.Services;
using Client.Models;

namespace Client.Pages.School.Baby.Blogs.BabyCarrier
{
    public partial class BlogPage
    {
        [Parameter]
        public string Id { get; set; }
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                _blogService.Id = "baby-carrier";
                _blog = await _blogService.GetByIdAsync(Id);
                if (_blog is null)
                {
                    _navigation.NavigateTo($"/school/baby/blogs/{_blogService.Id}");
                    return;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private Blog _blog { get; set; }
        [Inject]
        private BlogService _blogService { get; set; }
        [Inject]
        private NavigationManager _navigation { get; set; }
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }
    }
}
