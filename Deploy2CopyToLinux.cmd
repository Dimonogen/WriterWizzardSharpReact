cd DiplomBackApi

dotnet publish --configuration Release --os linux

cd ../client

::npm run build

cd ..

pscp -r -pw QazWsx12@ DiplomBackApi\DiplomBackApi\bin\Release\net8.0\linux-x64\publish user@192.168.1.42:/opt/loadBack

pscp -r -pw QazWsx12@ client\build\ user@192.168.1.42:/opt/loadFront

pause