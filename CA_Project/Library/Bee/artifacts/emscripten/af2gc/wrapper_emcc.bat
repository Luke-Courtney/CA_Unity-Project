
@echo off
"C:\Program Files\Unity\Hub\Editor\2021.3.22f1-x86_64\Editor\Data\PlaybackEngines\WebGLSupport\BuildTools\Emscripten\emscripten\emcc.bat" %* < nul
exit %ERRORLEVEL%
