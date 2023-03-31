export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalFiltered: number;
  totalPages: number;
}

export class PaginatedResult<T>{
  result!: T;
  pagination!: Pagination;
}
