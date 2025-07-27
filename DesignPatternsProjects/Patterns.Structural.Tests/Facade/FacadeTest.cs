using Xunit;

namespace Patterns.Structural.Facade.Tests
{
    public class FacadeTest
    {
        [Fact]
        public void TestFacadePattern()
        {
            // Créer tous les composants
            var tv = new TV();
            var dvd = new DVDPlayer();
            var audio = new AudioSystem();
            var lights = new Lights();

            // Créer la façade
            var homeTheater = new HomeTheaterFacade(tv, dvd, audio, lights);

            // Démarrer un film
            homeTheater.WatchMovie();

            // Vérifier l'état des composants
            var (testTv, testDvd, testAudio, testLights) = homeTheater.GetComponents();
            
            // La lumière devrait être tamisée à 20%
            Assert.Equal(20, testLights.GetBrightness());
            
            // Le volume devrait être à 40
            Assert.Equal(40, testAudio.GetVolume());

            // Terminer le film
            homeTheater.EndMovie();

            // Vérifier que la lumière est revenue à 100%
            Assert.Equal(100, testLights.GetBrightness());
        }
    }
}
