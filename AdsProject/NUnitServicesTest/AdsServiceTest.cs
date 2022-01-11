using NUnit.Framework;
using Repositories.Interfaces;
using Models.Models;
using Moq;
using System.Collections.Generic;
using Serviñes.Services;
using Repositories;
using System;
using System.Threading.Tasks;

namespace NUnitServicesTest
{
    [TestFixture]
    public class AdsServiceTest
    {
        private List<Ad> _goodAds;

        Mock<IAdsRepository> _mockAds;
        Mock<ICategoryRepository> _mockCategory;
        Mock<IUserRepository> _mockUser;

        private AdsService _adsService;

        delegate void AddAd(Ad ad, int id);
        [OneTimeSetUp]
        public void Setup()
        {
            _goodAds = new List<Ad>() {
            new Ad(){Id=1, Name = "Name 1", Title = "Title  1", Address = "Address 1",Image = "Image 1", Category = 1, Type = 1, State = 1 },
            new Ad(){Id=2, Name = "Name 2", Title = "Title  2", Address = "Address 2",Image = "Image 2", Category = 2, Type = 2, State = 2 },
            new Ad(){Id=3, Name = "Name 3", Title = "Title  3", Address = "Address 3",Image = "Image 3", Category = 3, Type = 1, State = 3 },
            };

            _mockAds = new Mock<IAdsRepository>();
            _mockCategory = new Mock<ICategoryRepository>();
            _mockUser = new Mock<IUserRepository>();

            UnitOfWork unitOfWork = new UnitOfWork(_mockAds.Object, _mockUser.Object, _mockCategory.Object);
            _adsService = new AdsService(unitOfWork);
        }

        [Test]
        public void Test_AddAd_GoodValues()
        {
            List<Ad> ads = new List<Ad>();
            _mockAds.Setup(m => m.AddAdAsync(It.IsAny<Ad>())).Callback((Ad x) => { ads.Add(x); });

            foreach (Ad ad in _goodAds)
            {
                _adsService.AddAdAsync(ad, 1);
            }
            int count = ads.Count;

            Assert.AreEqual(3, count);
        }

        [Test]
        public void Test_AddAd_BadValue1()
        {
            Ad ad = null;

            Assert.Throws<ArgumentException>(delegate { _adsService.AddAdAsync(ad, 1); });
        }

        [Test]
        public void Test_AddAd_BadValue2()
        {
            Ad ad = new Ad();

            Action action = () => _adsService.AddAdAsync(ad, 1);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }

        [Test]
        public void Test_AddAd_BadValue3()
        {
            Ad ad = new Ad() { Name = "Name", Title = "Title  1", Address = "Address 1", Image = "Image 1", Category = 1, Type = 1, State = 1 };

            Action action = () => _adsService.AddAdAsync(ad, 1);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }

        [Test]
        public void Test_ChangeAd_GoodValues()
        {
            List<Ad> ads = new List<Ad>();
            _mockAds.Setup(m => m.ChangAdAsync(It.IsAny<Ad>())).Callback((Ad x) => { ads.Add(x); });
            _mockAds.Setup(m => m.CheckAdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(1));

            foreach (Ad ad in _goodAds)
            {
                _adsService.ChangeAdAsync(ad, 1);
            }
            int count = ads.Count;

            Assert.AreEqual(3, count);
        }

        [Test]
        public void Test_ChangeAd_BadValue1()
        {
            Ad ad = null;

            Action action = () => _adsService.ChangeAdAsync(ad, 1);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }

        [Test]
        public void Test_ChangeAd_BadValue2()
        {
            Ad ad = new Ad();

            Action action = () => _adsService.ChangeAdAsync(ad, 1);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }

        [Test]
        public void Test_ChangeAd_BadValue3()
        {
            Ad ad = new Ad() { Name = "Name", Title = "Title  1", Address = "Address 1", Image = "Image 1", Category = 1, Type = 1, State = 1 };

            Action action = () => _adsService.ChangeAdAsync(ad, 1);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }

        [Test]
        public void Test_DeleteAd_GoodValues()
        {
            int result = 0;
            _mockAds.Setup(m => m.DeleteAdAsync(It.IsAny<int>())).Callback((int x) => { result++; });
            _mockAds.Setup(m => m.CheckAdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(1));

            foreach (Ad ad in _goodAds)
            {
                _adsService.DeleteAdAsync(ad.Id, 1);
            }            

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test_DeleteAd_BadValue1()
        {
            int id = 0;
            int user = 0;
            _mockAds.Setup(m => m.CheckAdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns((id > 0 && user > 0) ? Task.FromResult(1) : Task.FromResult(0));

            Action action = () => _adsService.DeleteAdAsync(id, user);

            Assert.Throws<ArgumentException>(delegate { action(); });
        }
    }
}