PROJECT_NAME ?= CoffeeBox
ORG_NAME ?= CoffeeBox
REPO_NAME ?= CoffeeBox

.PHONY: migrations db

migrations:
		cd ./CoffeeBox.Data && dotnet ef --startup-project ../CoffeeBox.Web/ migrations add $(mname) && cd ..

db:
		cd ./CoffeeBox.Data && dotnet ef --startup-project ../CoffeeBox.Web/ database update $(mname) && cd ..