export default interface QueryResult<T> {
  pageSize: number;
  totalRecords: number;
  currentPage: number;
  totalPages: number;
  data: T[];
}
