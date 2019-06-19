# import flask microframework library
from flask import Flask
from flask import jsonify
from flask import request
import json
import sys
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from pandas_funcs.create_dataframe.from_converted_dyn_dict import from_converted_dyn_dict
 
# initialize the flask application
app = Flask(__name__)
 
# endpoint api_1() with post method
@app.route("/api/v1.0/csharp_python_restfulapi_json", methods=["POST"])
def csharp_python_restfulapi_json():
    """
    simple c# test to call python restful api web service
    """
    try:                
        #get request json object
        request_json = request.get_json()
        print(request_json)      
        #convert to response json object 
        response = jsonify(request_json)
        print(response)
        response.status_code = 200  
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

@app.route("/api/v1.0/create_dataframe_from_dict", methods=["POST"])
def create_dataframe():
    try:
        request_json = request.get_json()
        print(request_json)
        df = from_converted_dyn_dict(request_json)
        df_json = df.to_json(orient='index')
        print("df_json" +df_json)
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response


if __name__ == "__main__":
#     run flask application in debug mode
    app.run(debug=True)