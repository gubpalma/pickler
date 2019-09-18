using Pickler.Interfaces;
using System.Threading.Tasks;

namespace Pickler.Application
{
    public class PicklerApplication : IApplication
    {
        private readonly IArgumentParser _argumentParser;
        private readonly IFileLoader _fileLoader;
        private readonly IFeatureParser _featureParser;
        private readonly ITrxResultsParser _trxResultsParser;
        private readonly IResultRenderer _resultRenderer;

        public PicklerApplication(
            IArgumentParser argumentParser,
            IFileLoader fileLoader,
            IFeatureParser featureParser,
            ITrxResultsParser trxResultsParser,
            IResultRenderer resultRenderer)
        {
            _argumentParser = argumentParser;
            _fileLoader = fileLoader;
            _featureParser = featureParser;
            _trxResultsParser = trxResultsParser;
            _resultRenderer = resultRenderer;
        }

        public async Task RunAsync(string[] args)
        {
            var options = _argumentParser.Parse(args);

            var featureFile = await _fileLoader.OpenAsync(options.FeatureInputFilePath);
            var featureData = _featureParser.ParseFeature(featureFile);

            var trxFile = await _fileLoader.OpenAsync(options.TrxInputFilePath);
            var trxData = _trxResultsParser.Build(trxFile);

            await _resultRenderer.RenderAsync(featureData, trxData, options.OutputFilePath);
        }
    }
}
