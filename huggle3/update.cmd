PATH=%PATH%;C:\Program Files (x86)\Git\bin
cd %1
git rev-list HEAD --count > %2
git describe --always >> %2
