using Pickler.Interfaces;
using System.Threading.Tasks;
using Pickler.Interfaces.Gherkin;
using Pickler.Interfaces.Trx;

namespace Pickler.Application
{
    public class PicklerApplication : IApplication
    {
        private readonly IArgumentParser _argumentParser;
        private readonly IFileLoader _fileLoader;
        private readonly IFeatureExtractor _featureExtractor;
        private readonly ITrxResultsParser _trxResultsParser;
        private readonly IResultRenderer _resultRenderer;

        public PicklerApplication(
            IArgumentParser argumentParser,
            IFileLoader fileLoader,
            IFeatureExtractor featureExtractor,
            ITrxResultsParser trxResultsParser,
            IResultRenderer resultRenderer)
        {
            _argumentParser = argumentParser;
            _fileLoader = fileLoader;
            _featureExtractor = featureExtractor;
            _trxResultsParser = trxResultsParser;
            _resultRenderer = resultRenderer;
        }

        public async Task RunAsync(string[] args)
        {
            var options = _argumentParser.Parse(args);

            var featureFile = await _fileLoader.OpenAsync(options.FeatureInputFilePath);
            var featureData = _featureExtractor.Extract(featureFile);

            var trxFile = await _fileLoader.OpenAsync(options.TrxInputFilePath);
            var trxData = _trxResultsParser.Build(trxFile);

            await _resultRenderer.RenderAsync(featureData, trxData, options.OutputFilePath);
        }
    }
}
