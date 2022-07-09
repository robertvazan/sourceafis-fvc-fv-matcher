// Part of SourceAFIS matcher for FVC FV: https://sourceafis.machinezoo.com/fvc
using System;
using System.Globalization;
using System.IO;

namespace SourceAFIS.FVC.FV.Matcher;

public class Program
{
    static int Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: match.exe <templatefile1> <templatefile2> <outputfile>");
            return 1;
        }
        double? shaped = null;
        try
        {
            var probe = new FingerprintTemplate(File.ReadAllBytes(args[0]));
            var candidate = new FingerprintTemplate(File.ReadAllBytes(args[1]));
            var score = new FingerprintMatcher(probe).Match(candidate);
            // We have to convert SourceAFIS score to FVC score in range 0..1.
            //
            // 1. SourceAFIS produces score S calibrated to be around -log10(FMR), i.e. FMR in deciban units.
            // 2. Assume FNMR 1% or -20dban.
            // 3. Calculate log-odds of FNMR:FMR as O = S - 20dban.
            // 4. Convert to bans: O := O / 10dban/ban,
            // 5. Convert to nats: O := O * ln(10) nat/ban.
            // 6. Pass log-odds through standard logistic function: 1 / (1 + e^-O).
            // 7. Simplify the expression to get 1 / (1 + 0.1^(S / 10 - 2)).
            //
            // Logistic function is used in order to fit score in range 0..1
            // with neutral cases (FMR = FNMR) centered at 0.5.
            //
            // In practice, this produces shaped scores as follows:
            // score 0 => shaped 1%
            // score 10 => shaped 10%
            // score 20 => shaped 50%
            // score 30 => shaped 90%
            // score 40 => shaped 99%
            shaped = 1 / (1 + Math.Pow(0.1, score / 10 - 2));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        try
        {
            File.AppendAllText(args[2], string.Format("{0} {1} {2} {3}",
                args[0], args[1], shaped != null ? "OK" : "FAIL", (shaped ?? 0).ToString(new CultureInfo("en-US", false).NumberFormat)));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return 1;
        }
        return 0;
    }
}
