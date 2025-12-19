"""
See YouTube tutorial here: https://youtu.be/Is8Qu7onvzM
"""
import random
import time

import bpy

from dataclasses import dataclass
from typing import List
################################################################
# helper functions BEGIN
################################################################


def purge_orphans():
    """
    Remove all orphan data blocks

    see this from more info:
    https://youtu.be/3rNqVPtbhzc?t=149
    """
    if bpy.app.version >= (3, 0, 0):
        # run this only for Blender versions 3.0 and higher
        bpy.ops.outliner.orphans_purge(do_local_ids=True, do_linked_ids=True, do_recursive=True)
    else:
        # run this only for Blender versions lower than 3.0
        # call purge_orphans() recursively until there are no more orphan data blocks to purge
        result = bpy.ops.outliner.orphans_purge()
        if result.pop() != "CANCELLED":
            purge_orphans()


def clean_scene():
    """
    Removing all of the objects, collection, materials, particles,
    textures, images, curves, meshes, actions, nodes, and worlds from the scene

    Checkout this video explanation with example

    "How to clean the scene with Python in Blender (with examples)"
    https://youtu.be/3rNqVPtbhzc
    """
    # make sure the active object is not in Edit Mode
    if bpy.context.active_object and bpy.context.active_object.mode == "EDIT":
        bpy.ops.object.editmode_toggle()

    # make sure non of the objects are hidden from the viewport, selection, or disabled
    for obj in bpy.data.objects:
        obj.hide_set(False)
        obj.hide_select = False
        obj.hide_viewport = False

    # select all the object and delete them (just like pressing A + X + D in the viewport)
    bpy.ops.object.select_all(action="SELECT")
    bpy.ops.object.delete()

    # find all the collections and remove them
    collection_names = [col.name for col in bpy.data.collections]
    for name in collection_names:
        bpy.data.collections.remove(bpy.data.collections[name])

    # in the case when you modify the world shader
    # delete and recreate the world object
    world_names = [world.name for world in bpy.data.worlds]
    for name in world_names:
        bpy.data.worlds.remove(bpy.data.worlds[name])
    # create a new world data block
    bpy.ops.world.new()
    bpy.context.scene.world = bpy.data.worlds["World"]

    purge_orphans()


def active_object():
    """
    returns the currently active object
    """
    return bpy.context.active_object


def time_seed():
    """
    Sets the random seed based on the time
    and copies the seed into the clipboard
    """
    seed = time.time()
    print(f"seed: {seed}")
    random.seed(seed)

    # add the seed value to your clipboard
    bpy.context.window_manager.clipboard = str(seed)

    return seed


def set_fcurve_extrapolation_to_linear():
    for fc in bpy.context.active_object.animation_data.action.fcurves:
        fc.extrapolation = "LINEAR"


def create_data_animation_loop(obj, data_path, start_value, mid_value, start_frame, loop_length, linear_extrapolation=True):
    """
    To make a data property loop we need to:
    1. set the property to an initial value and add a keyframe in the beginning of the loop
    2. set the property to a middle value and add a keyframe in the middle of the loop
    3. set the property the initial value and add a keyframe at the end of the loop
    """
    # set the start value
    setattr(obj, data_path, start_value)
    # add a keyframe at the start
    obj.keyframe_insert(data_path, frame=start_frame)

    # set the middle value
    setattr(obj, data_path, mid_value)
    # add a keyframe in the middle
    mid_frame = start_frame + (loop_length) / 2
    obj.keyframe_insert(data_path, frame=mid_frame)

    # set the end value
    setattr(obj, data_path, start_value)
    # add a keyframe in the end
    end_frame = start_frame + loop_length
    obj.keyframe_insert(data_path, frame=end_frame)

    if linear_extrapolation:
        set_fcurve_extrapolation_to_linear()


def set_scene_props(fps, frame_count):
    """
    Set scene properties
    """
    scene = bpy.context.scene
    scene.frame_end = frame_count

    # set the world background to black
    world = bpy.data.worlds["World"]
    if "Background" in world.node_tree.nodes:
        world.node_tree.nodes["Background"].inputs[0].default_value = (0, 0, 0, 1)

    scene.render.fps = fps

    scene.frame_current = 1
    scene.frame_start = 1


def scene_setup():
    fps = 30
    loop_seconds = 12
    frame_count = fps * loop_seconds

    seed = 0
    if seed:
        random.seed(seed)
    else:
        time_seed()

    clean_scene()

    set_scene_props(fps, frame_count)


################################################################
# helper functions END
################################################################

@dataclass
class Id:
    value: int       
    length: int      
    period: int      



class IdHandler:
    def __init__(self):
        self.counter = 0
        self.counter2 = 0
        self.ids: List[Id] = []

    def id_parser(self, filename: str):
        with open(filename, "r") as f:
            input_data = f.read()

        ranges = input_data.split(",")

        for r in ranges:
            start, end = map(int, r.split("-"))
            for value in range(start, end + 1):
                result = self.id_processor(str(value))
                if result:
                    self.counter += result
                    self.ids.append(
                        Id(
                            value=value,
                            length=len(str(value)),
                            period=self.find_period(str(value))
                        )
                    )

    def id_processor(self, id_str: str) -> int:
        checking_id = int(id_str)

        half = len(id_str) // 2
        first = id_str[:half]
        second = id_str[half:]

        if first == second:
            return checking_id

        for i in range(1, len(id_str)):
            pattern = id_str[:i]
            if pattern * (len(id_str) // i) == id_str:
                return checking_id

        return 0

    def find_period(self, s: str) -> int:
        n = len(s)
        for size in range(1, n // 2 + 1):
            if n % size == 0:
                if s[:size] * (n // size) == s:
                    return size
        return 0

def create_spheres_from_lock_data():
    handler = IdHandler()
    # Use absolute path to the input file
    file_path = r"C:\Users\elial\Documents\GitHub\Advent-Of-Code\Elia\DayTwoInput.txt"
    handler.id_parser(file_path)
    vertices = []
    scale = 0.03

    
    for i, num in enumerate(handler.ids):
        y = i * scale
        x = num.value * scale * 0.01
        z = num.period * scale
        vertices.append((x, y, z))

    mesh = bpy.data.meshes.new("IdData")
    mesh.from_pydata(vertices, [], [])

    point_obj = bpy.data.objects.new("IdDatas", mesh)
    bpy.context.collection.objects.link(point_obj)

    bpy.context.view_layer.objects.active = point_obj
    point_obj.select_set(True)
    
    # Create geometry nodes
    bpy.ops.node.new_geometry_nodes_modifier()
    
    modifier = point_obj.modifiers[-1]
    node_tree = modifier.node_group

    group_input = None
    group_output = None
    for node in node_tree.nodes:
        if node.type == 'GROUP_INPUT':
            group_input = node
        elif node.type == 'GROUP_OUTPUT':
            group_output = node
        bpy.ops.mesh.primitive_uv_sphere_add(radius=1.0, location=(0, 0, 0))
    sphere_obj = bpy.context.active_object
    sphere_obj.name = "InstanceSphere"
    sphere_obj.hide_viewport = True
    sphere_obj.hide_render = True

    obj_info = node_tree.nodes.new(type='GeometryNodeObjectInfo')
    obj_info.location = (200, -300)
    obj_info.inputs[0].default_value = sphere_obj

    instance_node = node_tree.nodes.new(type='GeometryNodeInstanceOnPoints')
    instance_node.location = (400, 0)
    mesh_to_points = node_tree.nodes.new(type='GeometryNodeMeshToPoints')

    node_tree.links.new(mesh_to_points.outputs['Points'], instance_node.inputs['Points'])



def main():
    """
    Python code to generate an animated geo nodes node tree
    that consists of a subdivided & triangulated cube with animated faces
    """
    scene_setup()
    create_spheres_from_lock_data()


if __name__ == "__main__":
    main()