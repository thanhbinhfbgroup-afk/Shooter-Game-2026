dotnet Luban.dll ^
    -t all ^
    -c cs-bin ^
    -d bin ^
    --conf "..\DesignerConfigs\luban.conf" ^
    -x outputCodeDir="..\Assets\Scripts\Gen" ^
    -x outputDataDir="..\Assets\Resources\ConfigData"
pause
