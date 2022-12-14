# A Small utility to add the include feature to glsl
## How to use it
* Download the latest release
* Usage example : GLSLInclude.exe filea.glsl fileb.glsl -o {fileNameNoExt}.generated{ext}
* This will output the file with includes copy pasted into the "generated" file where the include line lies.

## What it does
* Handle multiple includes in one file
* The syntax is the same as in C language
* Handle deep includes correctly
* Handle relative paths
* Detects circular references
* It does allow diamond shaped includes

## What it does not / have not been tested
* Absolute paths
* Not tested spaces / tab robustness
* Not tested comments robustness
* Not tested multiline include
* Not tested <> includes

## Roadmap
* Clean it a bit
* Handle line returns in a cleaner way
* Provide it as a single file dependency along the executable
* Support as many .net platform as possible (at least down to .net standard 1.0)
* Publish it as a nuget package
* More unit tests
* Open to suggestions :)

