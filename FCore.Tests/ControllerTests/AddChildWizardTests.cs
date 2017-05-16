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
        public void CheckCreatedMemberRelationshipWithCreatorRelatives() // signature to be fully detailed
        {
            // set
            MoqRepository moqRepo = new MoqRepository();
            var lior = moqRepo.GetFamilyMember(1);
            var keren = moqRepo.GetFamilyMember(2);

            lior.Relatives.Add(new RelativeModel(lior.Id, keren.Id, RelationshipType.Wife)
            {
               Id = 1,
               Member = lior,
               Relative = keren
            });

            keren.Relatives.Add(new RelativeModel(keren.Id, lior.Id, RelationshipType.Husband)
            {
                Id = 3,
                Member = keren,
                Relative = lior
            });

            // simulates the db connecting creator & posted
            var gaya = moqRepo.CreateMember(-1, null, "");
            lior.Relatives.Add(new RelativeModel(lior.Id, gaya.Id, RelationshipType.Daughter)
            {
                Member = lior,
                Relative = gaya
            });

            // act 
            // should find a creator's relative & check it's relationship to the created
            gaya = moqRepo.ConnectRelatives(lior, gaya);
            RelationshipType rel = RelationshipType.Undefined;
            foreach (var r in gaya.Relatives)
            {
                if (r.Relative == keren)
                {
                    rel = (RelationshipType)Enum.Parse(typeof(RelationshipType), r.Relationship);
                    break;
                }
            }

            // assert
            Assert.AreEqual(RelationshipType.Mother, rel);
        }

        [TestMethod]
        public void CheckDatabaseForProperUpdateOfNewMemberAndCurrentRelativeRelativesList()
        {
            // set

            // act

            // assert
        }
    }
}
