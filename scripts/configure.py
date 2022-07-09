# This script generates and updates project configuration files.

# We are assuming that project-config is available in sibling directory.
# Checkout from https://github.com/robertvazan/project-config
import pathlib
project_directory = lambda: pathlib.Path(__file__).parent.parent
config_directory = lambda: project_directory().parent/'project-config'
exec((config_directory()/'src'/'fvc.py').read_text())

benchmark_name = lambda: 'Fingerprint Verification'
benchmark_abbreviation = lambda: 'FV'
benchmark_url = lambda: 'https://biolab.csr.unibo.it/FVCOnGoing/UI/Form/BenchmarkAreas/BenchmarkAreaFV.aspx'
is_matcher_part = lambda: True
bundled_sister_projects = lambda: ['sourceafis-fvc-fv-extractor']
inception_year = lambda: 2022

generate()
