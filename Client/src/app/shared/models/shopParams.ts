export class ShopParams {
  brandId: number = 0;
  typeId: number = 0;
  sort = 'name';
  pageNumber: number = 1;
  pageSize: number = 6;
  search!: string;
}
