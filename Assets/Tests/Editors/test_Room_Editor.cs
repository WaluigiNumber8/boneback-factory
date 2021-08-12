using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Global.SafetyChecks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Tests.Editors
{
    public class test_Room_Editor
    {
        private LibraryOverseer lib;
        private PackBuilder packBuilder;
        private PackAsset pack;

        [SetUp]
        public void Setup()
        {
            lib = LibraryOverseer.Instance;

            string packName = "Test Pack";
            string packDescription = "Created this pack for testing purposes.";
            string packAuthor = "TestAuthor";
            Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

            packBuilder = new PackBuilder();
            pack = packBuilder.BuildPack(packName, packDescription, packAuthor, packIcon);
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                lib.Library.Remove(pack.PackInfo.packName, pack.PackInfo.author);
            }
            catch (SafetyNetException)
            {
                Debug.Log($"{pack.PackInfo.packName} does not exist anymore. It might have been deleted already.");
            }
        }

        [Test]
        public void create_new_room()
        {

        }
    }
}