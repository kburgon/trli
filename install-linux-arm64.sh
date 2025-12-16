#!env /bin/sh

dotnet publish src/Trli -c Release -r linux-arm64 --self-contained true -p:PublishSingleFile=true -p:AssemblyName=trli
echo 'Copying published app to .local/bin...'
cp src/Trli/bin/Release/net10.0/linux-arm64/publish/trli "${HOME}/.local/bin"
echo 'Installation complete.'
