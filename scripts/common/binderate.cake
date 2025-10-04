
//---------------------------------------------------------------------------------------
Task("holisticware-android-binderator")
    .Does
    (
        () =>
        {
            /*
             rm -fr generated externals output \
            && \
            holisticware-android-binderator \
                binderate \
                    --config-file ./config.json \
                    --base-path $(pwd) \
            */
            Process.Start
            (
                "holisticware-android-binderator",
                "binderate"
                + " " +
                $"--config-file {path_project}/config.json"
                + " " +
                $"--base-path {path_project}"
            );

            return;
        }
    );

//---------------------------------------------------------------------------------------
