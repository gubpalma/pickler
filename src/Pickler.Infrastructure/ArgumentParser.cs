using Pickler.Interfaces;
using Pickler.Interfaces.Options;

namespace Pickler.Infrastructure
{
    public class ArgumentParser : IArgumentParser
    {
        public PicklerOptions Parse(string[] args)
        {
            var featureInputFilePath = "insert here";
            var trxInputFilePath = "insert here";
            var outputFilePath = "insert here";

            return new PicklerOptions
            {
                FeatureInputFilePath = featureInputFilePath,
                TrxInputFilePath = trxInputFilePath,
                OutputFilePath = outputFilePath
            };
        }
    }
}
