﻿namespace ProjectManagement.Dtos
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
