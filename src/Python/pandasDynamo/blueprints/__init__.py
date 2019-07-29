from flask import Flask

app = Flask(__name__)

from blueprints.api.start.start import mod

from blueprints.api.dataframes.create_dataframe import mod
from blueprints.api.filters.filter_dataframe import mod
from blueprints.api.format.format_dataframe import mod
from blueprints.api.reshape.reshape_dataframe import mod
from blueprints.api.select.select_rows import mod
from blueprints.api.select.select_columns import mod
from blueprints.api.information.basic_information import mod
from blueprints.api.information.summary import mod
from blueprints.api.combine.combine_dataframe import mod

app.register_blueprint(api.start.start.mod)

app.register_blueprint(api.dataframes.create_dataframe.mod, url_prefix='/api/create_dataframe')
app.register_blueprint(api.filters.filter_dataframe.mod, url_prefix='/api/filter_dataframe')
app.register_blueprint(api.format.format_dataframe.mod, url_prefix='/api/format_dataframe')
app.register_blueprint(api.reshape.reshape_dataframe.mod, url_prefix='/api/reshape_dataframe')
app.register_blueprint(api.select.select_rows.mod, url_prefix='/api/select_rows')
app.register_blueprint(api.select.select_columns.mod, url_prefix='/api/select_columns')
app.register_blueprint(api.information.basic_information.mod, url_prefix='/api/basic_information')
app.register_blueprint(api.information.summary.mod, url_prefix='/api/summary')
app.register_blueprint(api.combine.combine_dataframe.mod, url_prefix='/api/combine')

from blueprints.api.error_handlers import blueprint

app.register_blueprint(api.error_handlers.blueprint)