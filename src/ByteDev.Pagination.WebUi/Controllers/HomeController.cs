using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ByteDev.Pagination.Presentation;
using ByteDev.Pagination.Presentation.PageOffSet;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 100;
        private const int TotalItemsCount = 201;
        private const int MaxPageNumbersToShow = 3;

        public ActionResult Index(int pageNumber = 0)
        {
            var viewModel = new IndexViewModel
            {
                PaginationPage = CreatePaginationPageViewModel(pageNumber)
            };
            return View(viewModel);
        }

        private static PaginationPageViewModel CreatePaginationPageViewModel(int pageNumber)
        {
            var pagingInfo = new PresentationPagingInfo(TotalItemsCount, PageSize, pageNumber, MaxPageNumbersToShow);

            return new PaginationPageViewModel(TotalItemsCount, pageNumber, PageSize)
            {
                PageNavigationNumbers = new PageNumbersFactory(new MiddlePageOffSetStrategy()).Create(pagingInfo)
            };
        }
    }
}
