from flask import Blueprint
from flask import current_app as app
from flask import jsonify
from utillities.exception_class import EmptyDataframe

blueprint = Blueprint('error_handlers', __name__)

@blueprint.app_errorhandler(EmptyDataframe)
def handle_invalid_usage(error):
    response = jsonify(error.to_dict())
    response.status_code = error.status_code
    return response