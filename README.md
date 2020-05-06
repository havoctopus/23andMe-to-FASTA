# 23andMe to FASTA

## About
If you have a 23andMe raw data file which contains mt-DNA data with refSNPs/RSID but not in FASTA file format, this tool will help you. Prebuilt executables are available in [releases](https://github.com/fiidau/23andMe-to-FASTA/releases/latest)

## Usage
Open the 23andMe raw data and save the mtDNA FASTA file. Once saved, you can use [FASTA to RSRS - With Visualizer](https://github.com/fiidau/FASTA-to-RSRS-with-Visualizer) to get RSRS markers and visualize the mutations from RSRS. You can also use James Lick's [mtDNA Haplogroup analysis](http://dna.jameslick.com/mthap/).

## Assumption
23andMe mtDNA raw data uses rCRS as reference (and positions for v2 and v3) and covers all variations from rCRS. If my assumption is wrong, please do alert me and I can fix the tool.
