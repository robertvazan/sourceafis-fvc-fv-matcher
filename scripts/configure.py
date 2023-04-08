# This script generates and updates project configuration files.

# Run this script with rvscaffold in PYTHONPATH
import rvscaffold as scaffold

class Project(scaffold.Fvc):
    def script_path_text(self): return __file__
    def benchmark_name(self): return 'Fingerprint Verification'
    def benchmark_abbreviation(self): return 'FV'
    def benchmark_url(self): return 'https://biolab.csr.unibo.it/FVCOnGoing/UI/Form/BenchmarkAreas/BenchmarkAreaFV.aspx'
    def is_matcher_part(self): return True
    def bundled_sister_projects(self): return ['sourceafis-fvc-fv-extractor']
    def inception_year(self): return 2022
    def project_status(self): return self.stable_status()

Project().generate()
