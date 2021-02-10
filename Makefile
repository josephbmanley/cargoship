
PROJECTNAME="Cargoship"

# Export Godot Project
godot-build:
	@echo "  >  Building binary..."
	@mkdir -p ./builds
	@godot --export Linux/X11 builds/Cargoship.x86_64


## run: Build and run project
run: godot-build
	@./builds/Cargoship.x86_64

## build: Export godot project
build: godot-build

## help: Displays help text for make commands
.DEFAULT_GOAL := help
all: help
help: Makefile
	@echo " Choose a command run in "$(PROJECTNAME)":"
	@sed -n 's/^##//p' $< | column -t -s ':' |  sed -e 's/^/ /'