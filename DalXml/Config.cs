﻿namespace Dal;

internal static class Config
{

    static string s_data_config_xml = "data-config";
    internal static int NextTadkId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextCourseId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextLinkId"); }

}