﻿namespace DeemZ.Models.ViewModels.Administration
{
    public class PagingBaseModel
    {
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int MaxPages { get; set; }
    }
}