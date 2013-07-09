if defined ProgramFiles(x86) (
PATH=%PATH%;%ProgramFiles(x86)%\Git\bin
) else (
PATH=%PATH%;%ProgramFiles%\Git\bin
)
cd %1
git rev-list HEAD --count > %2
git describe --always >> %2
