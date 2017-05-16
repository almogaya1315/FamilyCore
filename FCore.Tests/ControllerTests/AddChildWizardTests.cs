using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FCore.Tests.Moq;
using FCore.Common.Models.Members;
using FCore.Common.Enums;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;

namespace FCore.Tests.ControllerTests
{
    [TestClass]
    public class AddChildWizardTests
    {
        [TestMethod]
        public void ConnectRelatives() // signature to be fully detailed
        {
            MoqRepository moqRepo = new MoqRepository();
            var lior = moqRepo.GetFamilyMember(1);
            var keren = moqRepo.GetFamilyMember(2);
            var gaya = moqRepo.CreateMember(1, null, RelationshipType.Daughter.ToString());
            
            moqRepo.ConnectRelatives();
        }
    }
}
