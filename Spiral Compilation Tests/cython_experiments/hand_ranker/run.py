import numpy as np
import pyximport
pyximport.install(language_level=3,setup_args={"include_dirs":np.get_include()})
from runner import main
print(main())