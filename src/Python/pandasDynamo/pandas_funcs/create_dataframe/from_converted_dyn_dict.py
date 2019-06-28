import json
import pandas as pd

# Create Dataframes
def from_converted_dyn_dict(data_dict):
    try:
        dataframe = pd.DataFrame(eval(data_dict))
        return dataframe
    except Exception as e:
        return e


