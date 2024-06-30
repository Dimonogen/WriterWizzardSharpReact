set dev=.env
set tran=dev.env
set build=build.env

::echo "Test" %dev% %tran%

rename %dev% %tran%
rename %build% %dev%

timeout 1 > NUL

call npm run build

::ping dimonogen.ru

timeout 1 > NUL

rename %dev% %build%
rename %tran% %dev%

pause