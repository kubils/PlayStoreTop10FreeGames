using Application.Mapping;
using AutoMapper;

namespace UnitTests
{
    public class CommonClassFixture
    {
        public IMapper Mapper { get; set; }
        public CommonClassFixture()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            }).CreateMapper();
        }  
    }
}