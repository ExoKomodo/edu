using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Client.Services;
using Client.Models;

namespace Client.Pages.Blogs.General
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
                _blogService.Id = "general";
                _blog = await _blogService.GetByIdAsync(Id);
                if (_blog is null)
                {
                    _navigation.NavigateTo("/blogs/general");
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
