using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDatastore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDatastore Current { get;}=new CitiesDatastore();
          
        public CitiesDatastore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() { Id = 1,
                    Name = "new york", 
                    Description="The on is pig park",
                    PointOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "center ",
                            Description="The on is pig park"},
                         new PointOfInterestDto(){
                            Id = 2,
                            Name = "top ",
                            Description="The on is pig park"}
                    }
                
                },
                new CityDto() {
                    Id = 2,
                    Name = "tanta", 
                    Description="The on is pig fs" ,
                    PointOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "center ",
                            Description="The on is pig park"},
                        new PointOfInterestDto(){
                            Id = 2,
                            Name = "top ",
                            Description="The on is sdsd park"},
                        new PointOfInterestDto(){
                            Id = 3,
                            Name = "bnn ",
                            Description="The on is ghjh park"},
                        new PointOfInterestDto(){
                            Id = 4,
                            Name = "tonfhgjnp ",
                            Description="The on is hjhj park"}
                    }

                },
                new CityDto() { Id = 3, 
                    Name = "kornash",
                    Description="The on is pig fd" ,
                    PointOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "center ",
                            Description="The on is pig park"},
                         new PointOfInterestDto(){
                            Id = 2,
                            Name = "top ",
                            Description="The on is pig park"}
                    }
                },
                new CityDto() { Id = 4,
                    Name = "giza",
                    Description="The on is pig g",
                    PointOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "center ",
                            Description="The on is pig park"},
                         new PointOfInterestDto(){
                            Id = 2,
                            Name = "top ",
                            Description="The on is pig park"}
                    }
                },
                new CityDto() { Id = 5,
                    Name = "aswan",
                    Description="The on is pig m",
                    PointOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "center ",
                            Description="The on is pig park"},
                         new PointOfInterestDto(){
                            Id = 2,
                            Name = "top ",
                            Description="The on is pig park"}
                    }
                },
            };
            
            }
    }
}
