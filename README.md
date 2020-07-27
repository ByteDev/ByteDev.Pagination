[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Pagination.svg)](https://www.nuget.org/packages/ByteDev.Pagination)

# ByteDev.Pagination

Provides a simple framework for handling data pagination from within any kind of .NET system.

## Installation

ByteDev.Pagination has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Pagination is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Pagination`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Pagination/).


## Usage

The project is broken into two main namespaces: Data and Presentation.

### Data

Types in the Data namespace are concerned with getting a paginatable representation of your required data set.

`DataPagingInfo` represents the input data required for pagination by the repository/DAL layer.

`EntitiesPagingContainer` represents the output data returned from a repository/DAL layer.

Example usage from within a repository class:

```csharp
public class CustomerRepository
{
	public EntitiesPagingContainer<Customer> GetBySurname(string surname, DataPagingInfo pagingInfo)
	{
		var count = _dal.Customers.Count(c => c.Surname == surname);

		var customers = _dal.Customers.Where(c => c.Surname == surname)
			.OrderByDescending(c => c.DateTimeCreated)
			.Skip(pagingInfo.Skip)
			.Take(pagingInfo.PageSize)
			.ToList();

		return new EntitiesPagingContainer<Customer>(customers, count, pagingInfo);
	}
}
```

### Presentation

Types in the Presentation namespace are concerned with how you wish to handle pagination in the presentation layer/UI.

Once we have got a `EntitiesPagingContainer` back from the repository it needs to be converted to a `PaginationPageViewModel`.  `PaginationPageViewModel` represents a container for all the possible requirements for pagination.  It can easily be used as a Model for a MVC View/PartialView.  

For example:

```csharp
@model ByteDev.Pagination.Presentation.PaginationPageViewModel

@if (Model.HasMoreThanOnePage)
{
    <div>
        @if (Model.IsFirstPage)
        {
            <span>&lt;&lt;</span>
            <span>&lt;</span>
        }
        else
        {
            <a href="@Url.Action("Index", "Home", new {pageNumber = Model.FirstPageNumber})">&lt;&lt;</a>
            <a href="@Url.Action("Index", "Home", new {pageNumber = Model.PreviousPageNumber})">&lt;</a>
        }

        @foreach (var pageNumber in Model.PageNavigationNumbers)
        {
            if (pageNumber.IsCurrentPage)
            {
                <span>@pageNumber.DisplayNumber</span>
            }
            else
            {
                <a href="@Url.Action("Index", "Home", new {pageNumber = pageNumber.Number})">@pageNumber.DisplayNumber</a>
            }
        }

        @if (Model.IsLastPage)
        {
            <span>&gt;</span>
            <span>&gt;&gt;</span>
        }
        else
        {
            <a href="@Url.Action("Index", "Home", new {pageNumber = Model.NextPageNumber})">&gt;</a>
            <a href="@Url.Action("Index", "Home", new {pageNumber = Model.LastPageNumber})">&gt;&gt;</a>
        }
    </div>
}
```

For further example details see the ByteDev.Pagination.WebUi project.

