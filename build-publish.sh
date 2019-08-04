# This script is outdated. It was valid when WebApp and ClientApp were
# in separate Visual Studio projects.

GOV="/C/GOVMEETING/_SOURCECODE"
GOV_SER="${GOV}/src/Server/WebApp"
GOV_CLI="${GOV}/src/Client/ClientApp"

PUBLISH="${GOV_SER}/bin/Release/PublishOutput"

cd ${GOV_CLI}
echo "Run production build of ClientApp"
npm run build.prod

cd ${GOV_CLI}/dist/prod
echo "Move css files to ${PUBLISH}/wwwroot/css"
cp -f css/main.css ${PUBLISH}/wwwroot/css
echo "Move js files to ${PUBLISH}/wwwroot/css"
cp -f js/app.js    ${PUBLISH}/wwwroot/js
cp -f js/shims.js  ${PUBLISH}/wwwroot/js

echo "Move appsettings.Production.json to ${PUBLISH}"
cd ${GOV}/../_SECRETS
cp appsettings.Production.json ${PUBLISH}