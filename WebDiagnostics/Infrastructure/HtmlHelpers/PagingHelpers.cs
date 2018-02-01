using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using WebDiagnostics.ViewModels;

namespace WebDiagnostics.Infrastructure.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());

        }


        //generates Html page links on every Filter change
        //if there are > 5 pages we add 1st, PRevious, Next, and Last links
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo model)
        {
            StringBuilder result = new StringBuilder();

            if (model.TotalPages > 0)
            {

                TagBuilder selectDiv = new TagBuilder("div");
                selectDiv.AddCssClass("pull-left");

                //1st page link
                result.Append(BuildPagingLink(PagingLinkDirection.First, 1));
                //previous page link
                result.Append(BuildPagingLink(PagingLinkDirection.Previous,
                    model.CurrentPage == 1 ? 1 : (model.CurrentPage - 1)));

                //page size options
                var selectListItems = new List<SelectListItem>
                {
                    new SelectListItem {Text = "5", Value = "5", Selected = (model.ItemsPerPage == 5)},
                    new SelectListItem {Text = "10", Value = "10", Selected = (model.ItemsPerPage == 10)},
                    new SelectListItem {Text = "20", Value = "20", Selected = (model.ItemsPerPage == 20)},
                    new SelectListItem {Text = "50", Value = "50", Selected = (model.ItemsPerPage == 50)}
                };

                //build the items per page selector
                TagBuilder pageSizeDropDown = new TagBuilder("select");
                pageSizeDropDown.AddCssClass("form-control");
                pageSizeDropDown.MergeAttribute("id", "PageSize");
                pageSizeDropDown.MergeAttribute("name", "PagingInfo.PageSize");

                foreach (var selectListItem in selectListItems)
                {
                    TagBuilder option = new TagBuilder("option");
                    option.MergeAttribute("value", selectListItem.Value);
                    option.InnerHtml = selectListItem.Text;
                    if (selectListItem.Selected)
                    {
                        option.MergeAttribute("selected", "selected");
                    }
                    pageSizeDropDown.InnerHtml += option.ToString();
                }

                selectDiv.InnerHtml = pageSizeDropDown.ToString();
                result.Append(selectDiv.ToString());

                //build the next and last links
                //Next page link
                result.Append(BuildPagingLink(PagingLinkDirection.Next,
                    model.CurrentPage == model.TotalPages ? model.CurrentPage : (model.CurrentPage + 1)));
                //Last page link
                result.Append(BuildPagingLink(PagingLinkDirection.Last, model.TotalPages));

            }

            return MvcHtmlString.Create(result.ToString());
        }


        //generates Html page links on every Filter change, keeps list of 5 page umbers as well as 1st, and Last buttons
        //if there are > 5 pages we add 1st, PRevious, Next, and Last links
        public static MvcHtmlString PageLinksOriginal(this HtmlHelper html,
            PagingInfo model)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder selectDiv = new TagBuilder("div");
            selectDiv.AddCssClass("pull-left");

            //build the items per page drop down - default it to 10
            TagBuilder pageSizeDropDown = new TagBuilder("select");
            pageSizeDropDown.AddCssClass("form-control");
            pageSizeDropDown.MergeAttribute("id", "PageSize");
            pageSizeDropDown.MergeAttribute("name", "PagingInfo.PageSize");

            var selectListItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "5", Value = "5", Selected = (model.ItemsPerPage == 5 ) },
                new SelectListItem {Text = "10", Value = "10", Selected = (model.ItemsPerPage == 10 )},
                new SelectListItem {Text = "20", Value = "20", Selected = (model.ItemsPerPage == 20 )},
                new SelectListItem {Text = "50", Value = "50", Selected = (model.ItemsPerPage == 50 )}
            };

            foreach (var selectListItem in selectListItems)
            {
                TagBuilder option = new TagBuilder("option");
                option.MergeAttribute("value", selectListItem.Value);
                option.InnerHtml = selectListItem.Text;
                if (selectListItem.Selected)
                {
                    option.MergeAttribute("selected", "selected");
                }
                pageSizeDropDown.InnerHtml += option.ToString();
            }

            var labelTag = new TagBuilder("label");
            labelTag.InnerHtml = "Items Per Page";
            labelTag.MergeAttribute("style", "font-weight: normal;");

            selectDiv.InnerHtml = pageSizeDropDown.ToString() + labelTag.ToString();
            result.Append(selectDiv.ToString());

            if (model.TotalPages > 5)
            {
                //1st page link
                result.Append(BuildPagingLink(PagingLinkDirection.First, 1));
                //previous page link
                result.Append(BuildPagingLink(PagingLinkDirection.Previous, model.CurrentPage == 1 ? 1 : (model.CurrentPage - 1)));
            }
            //build numbered links
            for (int i = 1; i <= model.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("input");
                tag.MergeAttribute("type", "button");
                tag.MergeAttribute("value", i.ToString());
                tag.MergeAttribute("pagenumber", i.ToString());
                tag.AddCssClass("paging-button");
                tag.AddCssClass("paging-link");
                tag.MergeAttribute("id", "btnPageLink" + i.ToString());
                if (i == model.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                //hide links after the 1st 5 (we only show 5 at ay one time)
                if (i > 5) { tag.AddCssClass("hidden"); }
                result.Append(tag.ToString());
            }

            //build the next and last links
            if (model.TotalPages > 5)
            {
                //Next page link
                result.Append(BuildPagingLink(PagingLinkDirection.Next, model.CurrentPage == model.TotalPages ? model.CurrentPage : (model.CurrentPage + 1)));
                //Last page link
                result.Append(BuildPagingLink(PagingLinkDirection.Last, model.TotalPages));
            }
            return MvcHtmlString.Create(result.ToString());
        }

        private static string BuildPagingLink(PagingLinkDirection direction, int pageNumber)
        {
            StringBuilder innerHtml = new StringBuilder();
            TagBuilder icon = new TagBuilder("i");
            TagBuilder tag = new TagBuilder("button");

            switch (direction)
            {
                case PagingLinkDirection.First:
                    icon.AddCssClass("fa fa-angle-double-left");
                    tag.MergeAttribute("id", "btnPageLinkFirst");
                    break;
                case PagingLinkDirection.Previous:
                    icon.AddCssClass("fa fa-angle-left");
                    tag.MergeAttribute("id", "btnPageLinkPrevious");
                    break;
                case PagingLinkDirection.Next:
                    icon.AddCssClass("fa fa-angle-right");
                    tag.MergeAttribute("id", "btnPageLinkNext");
                    break;
                case PagingLinkDirection.Last:
                    icon.AddCssClass("fa fa-angle-double-right");
                    tag.MergeAttribute("id", "btnPageLinkLast");
                    break;

            }

            innerHtml.Append(icon.ToString());
            tag.MergeAttribute("pagenumber", pageNumber.ToString());
            tag.AddCssClass("paging-button");
            tag.AddCssClass("btn btn-default");
            tag.InnerHtml = innerHtml.ToString();
            return tag.ToString();

        }

        private enum PagingLinkDirection
        {
            First,
            Previous,
            Next,
            Last

        }

    }
}




//public static MvcHtmlString PageLinks(this HtmlHelper html,
//    TaskViewModel model,
//    Func<int, string> pageUrl)
//{
//    StringBuilder result = new StringBuilder();
//    for (int i = 1; i <= model.PagingInfo.TotalPages; i++)
//    {
//        TagBuilder tag = new TagBuilder("a");
//        tag.MergeAttribute("href", pageUrl(i));
//        tag.InnerHtml = i.ToString();
//        if (i == model.PagingInfo.CurrentPage)
//        {
//            tag.AddCssClass("selected");
//            tag.AddCssClass("btn-primary");
//        }
//        tag.AddCssClass("btn btn-default");
//        result.Append(tag.ToString());
//    }
//    return MvcHtmlString.Create(result.ToString());

//}