#!/bin/sh

git rev-list HEAD --count > version.txt
git describe --always >> version.txt
