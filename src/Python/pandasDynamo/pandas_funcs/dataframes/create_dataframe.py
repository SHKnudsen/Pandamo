import json
import pandas as pd

# Create Dataframes
def by_dict(data_dict):
    try:
        dataframe = pd.DataFrame(data_dict)
        return dataframe
    except Exception as e:
        return e
