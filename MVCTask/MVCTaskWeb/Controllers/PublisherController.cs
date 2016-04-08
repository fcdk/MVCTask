using System.Web.Mvc;
using AutoMapper;
using MVCTask.Models.Publisher;
using MVCTaskModel.UnitOfWork;

namespace MVCTask.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ViewResult Details(string name)
        {
            var publisher = _unitOfWork.Publishers.GetPublisherByName(name);
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            return View(publisherViewModel);
        }

    }
}