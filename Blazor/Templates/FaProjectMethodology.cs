namespace Fran.Templates;

/// <summary>
/// Project delivery methodology / execution form.
/// </summary>
public enum FaProjectMethodology
{
    /// <summary>Agile framework with fixed-length sprints, ceremonies, and product backlog.</summary>
    AgileScrum,

    /// <summary>Agile framework with continuous flow, WIP limits, and visual board.</summary>
    AgileKanban,

    /// <summary>Traditional sequential phased delivery (Requirements, Design, Implementation, Verification, Maintenance).</summary>
    Waterfall,

    /// <summary>Hybrid combining Scrum structure with Kanban continuous flow.</summary>
    Scrumban,

    /// <summary>Agile methodology emphasizing engineering practices, TDD, pair programming, and frequent releases.</summary>
    ExtremeProgramming,

    /// <summary>Fixed-time, variable-scope cycles with appetite-driven pitching and betting.</summary>
    ShapeUp,

    /// <summary>Blended model combining Waterfall governance and architecture with Agile sprint execution.</summary>
    Hybrid
}

/// <summary>
/// Extension methods for <see cref="FaProjectMethodology"/>.
/// </summary>
public static class FaProjectMethodologyExtensions
{
    /// <summary>Returns human-readable display name for the methodology.</summary>
    public static string ToDisplayName(this FaProjectMethodology methodology) => methodology switch
    {
        FaProjectMethodology.AgileScrum => "Agile (Scrum)",
        FaProjectMethodology.AgileKanban => "Agile (Kanban)",
        FaProjectMethodology.Waterfall => "Waterfall (Phased Delivery)",
        FaProjectMethodology.Scrumban => "Scrumban",
        FaProjectMethodology.ExtremeProgramming => "Extreme Programming (XP)",
        FaProjectMethodology.ShapeUp => "Shape Up",
        FaProjectMethodology.Hybrid => "Hybrid (Agile / Waterfall)",
        _ => methodology.ToString()
    };

    /// <summary>Returns brief descriptive overview of the methodology.</summary>
    public static string ToDescription(this FaProjectMethodology methodology) => methodology switch
    {
        FaProjectMethodology.AgileScrum => "Time-boxed iterations (sprints), backlog refinement, sprint planning, daily standups, sprint reviews, and retrospectives.",
        FaProjectMethodology.AgileKanban => "Continuous pull system, work-in-progress (WIP) limits, cycle time tracking, and on-demand delivery.",
        FaProjectMethodology.Waterfall => "Structured, gated sequential phases with formal sign-offs across Discovery, Architecture, Build, QA, and Release.",
        FaProjectMethodology.Scrumban => "Iterative sprints with WIP limits and visual boards, balancing structured cadences with flow optimization.",
        FaProjectMethodology.ExtremeProgramming => "Continuous automated testing, test-driven development (TDD), code reviews, pair programming, and daily deployments.",
        FaProjectMethodology.ShapeUp => "Six-week project cycles with two-week cool-downs, shaped pitches, and autonomous betting table commitments.",
        FaProjectMethodology.Hybrid => "Sequential architectural and compliance discovery upfront, followed by iterative sprint-based execution and releases.",
        _ => ""
    };
}
