using CarWebService.BLL.Models.View;

namespace CarWebService.API.Models
{
    public class PaginateResponse
    {
        public int TotalItems { get; set; }
        public List<CarVm> Cars { get; set; }
    }
}
