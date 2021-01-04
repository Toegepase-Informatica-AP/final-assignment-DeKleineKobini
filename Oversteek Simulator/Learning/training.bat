@echo off

SET /p YAML="Enter config: "
SET /p ID="Enter ID: "

mlagents-learn %YAML% --run-id %ID%
pause