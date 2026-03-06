using AutoMapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product,ProductDto>().ReverseMap();
        CreateMap<Product,CreateProductDto>().ReverseMap();
        CreateMap<Product,UpdateProductDto>().ReverseMap();
    }
}