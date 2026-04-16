#!/bin/bash

echo "🧹 Limpiando resultados anteriores..."
rm -rf ./bin/Debug/net9.0/allure-results
rm -rf allure-report

echo "🧪 Corriendo tests..."
dotnet test

echo "📊 Generando reporte Allure..."
allure generate ./bin/Debug/net9.0/allure-results --clean -o allure-report

echo "🚀 Abriendo reporte..."
allure open allure-report
