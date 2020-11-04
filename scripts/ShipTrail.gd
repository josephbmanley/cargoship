extends Line2D

var offset_pos

var target
var point
export(NodePath) var targetPath
export var trailLength = 0

func _ready():
	offset_pos = position
	target = get_node(targetPath)

func _process(delta):
	global_position = Vector2(0,0)
	global_rotation = 0
	point = target.global_position + offset_pos.rotated(target.rotation)
	#rotation = target.rotation
	add_point(point)
	while get_point_count() > trailLength:
		remove_point(0)
